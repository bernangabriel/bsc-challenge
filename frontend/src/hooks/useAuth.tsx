import { createContext, useCallback, useContext, useMemo } from "react";
import { useNavigate } from "react-router-dom";

// hooks
import { useLocalStorage } from "./useLocalStorage";

// utils
import * as Constants from "../utils/Constants";

const AuthContext = createContext<{
  accessToken: string;
  login: (accessToken: string) => Promise<void>;
  logout: () => void;
} | null>(null);

export const AuthProvider = ({ children }: { children: React.ReactNode }) => {
  const [accessToken, setAccessToken] = useLocalStorage(
    Constants.storageKeys.ACCESS_TOKEN_KEY,
    ""
  );
  const navigate = useNavigate();

  const login = useCallback(
    async (accessToken: string) => {
      setAccessToken(accessToken);
      navigate("/");
    },
    [setAccessToken, navigate]
  );

  const logout = useCallback(() => {
    setAccessToken(null);
    localStorage.clear();
    navigate("/login", { replace: true });
  }, [setAccessToken, navigate]);

  const value = useMemo(
    () => ({
      accessToken,
      login,
      logout,
    }),
    [login, logout, accessToken]
  );
  return <AuthContext.Provider value={value}>{children}</AuthContext.Provider>;
};

export const useAuth = () => {
  return useContext(AuthContext);
};
