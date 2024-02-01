import { useEffect, useMemo } from "react";
import { Routes, Route, Link, Outlet } from "react-router-dom";
import { confirmAlert } from "react-confirm-alert";

// hooks
import { AuthProvider, useAuth } from "./hooks/useAuth";
import { useLocalStorage } from "./hooks/useLocalStorage";

// utils
import * as Constants from "./utils/Constants";

// types
import { UserProfileInfoDto } from "./types";

// services
import { getProfileInformation as _getProfileInformation } from "./services/AppService";

// pages
import LoginPage from "./pages/login/LoginPage";
import HomePage from "./pages/home/HomePage";
import EventLogPage from "./pages/event-log/EventLogPage";
import UserPage from "./pages/user/UserPage";

function App() {
  return (
    <AuthProvider>
      <Routes>
        <Route element={<LoginPage />} path="/login" />
        <Route element={<Layout />}>
          <Route path="/" element={<HomePage />} />
          <Route path="/login" element={<LoginPage />} />
          <Route path="/users" element={<UserPage />} />
          <Route path="/events" element={<EventLogPage />} />
        </Route>
      </Routes>
    </AuthProvider>
  );
}

function Layout() {
  const { logout } = useAuth();

  const profileInfoSaved =
    localStorage.getItem(Constants.storageKeys.USER_PROFILE_INFORMATION) || "";

  const [profileInformation, setProfileInformation] = useLocalStorage(
    Constants.storageKeys.USER_PROFILE_INFORMATION,
    profileInfoSaved
  );
  useEffect(() => {
    const fetchData = async () => {
      if (!profileInfoSaved) {
        const response = await _getProfileInformation();
        if (response?.data) {
          setProfileInformation(JSON.stringify(response.data));
        }
        location.reload();
      }
    };
    fetchData();
  }, [profileInfoSaved, setProfileInformation]);

  const userInfo = useMemo(
    () => profileInformation as UserProfileInfoDto,
    [profileInformation]
  );

  const onSignOuthandler = () => {
    confirmAlert({
      title: "Salir del Sistema",
      message: "¿Está seguro que desea salir del sistema?",
      buttons: [
        {
          label: "Sí",
          className: "btn btn-danger",
          onClick: () => logout(),
        },
        {
          label: "No",
          onClick: () => {},
        },
      ],
    });
  };

  return (
    <div>
      <nav
        className="navbar navbar-expand-lg navbar-dark"
        style={{ backgroundColor: "#34495e" }}
      >
        <div className="container-fluid">
          <Link className="nav-link active text-white fs-3 me-3" to="/">
            BSC
          </Link>
          <button
            className="navbar-toggler"
            type="button"
            data-bs-toggle="collapse"
            data-bs-target="#navbarNav"
            aria-controls="navbarNav"
            aria-expanded="false"
            aria-label="Toggle navigation"
          >
            <span className="navbar-toggler-icon"></span>
          </button>
          <div className="collapse navbar-collapse" id="navbarNav">
            <ul className="navbar-nav">
              <li className="nav-item">
                <Link className="nav-link active" to="/">
                  Inicio
                </Link>
              </li>
              <li className="nav-item">
                <Link className="nav-link active" to="/users">
                  Manejo de Usuarios
                </Link>
              </li>
              <li className="nav-item">
                <Link className="nav-link active" to="/events">
                  Eventos de Sistema
                </Link>
              </li>
            </ul>
            <span className="navbar-text ms-auto d-flex flex-row align-items-center">
              <em
                className="fa fa-sign-out fa-2x text-danger me-4 user-select-all"
                role="button"
                onClick={onSignOuthandler}
              />
              <div className="me-3">
                <div className="fw-bold text-white">
                  {userInfo.fullName} <span>({userInfo?.userName})</span>
                </div>
                <span>{userInfo.email}</span>
              </div>
            </span>
          </div>
        </div>
      </nav>
      <div className="p-3">
        <Outlet />
      </div>
    </div>
  );
}

export default App;
