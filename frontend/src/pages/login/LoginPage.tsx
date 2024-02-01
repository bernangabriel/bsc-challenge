import React, { useEffect } from "react";
import { useFormik } from "formik";
import * as Yup from "yup";
import { toast } from "react-toastify";

// common components
import { BaseButton, BaseInput } from "../../components/common";
import { UserLoginForm } from "@/types";

// services
import { signIn as _signIn } from "../../services/AppService";

// hooks
import { useAuth } from "../../hooks/useAuth";

interface Props {}

// validation schema
const validationSchema = Yup.object().shape({
  userName: Yup.string().required("El nombre de usuario es requerido."),
  password: Yup.string().required("La contraseña es requerida."),
});

const LoginPage: React.FC<Props> = (): React.ReactElement => {
  const { login } = useAuth();

  const initialValues: UserLoginForm = {
    userName: "",
    password: "",
  };

  const showToast = (message: string) => {
    toast.error(message, {
      autoClose: 3000,
      hideProgressBar: true,
      toastId: "userManagement",
    });
  };

  const onSubmitHandler = async (values: UserLoginForm) => {
    toast.clearWaitingQueue();
    const response = await _signIn(values);
    if (response?.data) {
      const userResult = response.data;
      if (!userResult.isValid) {
        showToast("Usuario o contraseña incorrectos.");
        return;
      }
      login(userResult.accessToken);
    }
  };

  const formik = useFormik({
    initialValues: initialValues,
    validationSchema: validationSchema,
    onSubmit: onSubmitHandler,
  });

  useEffect(() => {
    if (!formik.isValid && !formik.isValidating && formik.isSubmitting) {
      showToast("Favor completar los campos requeridos.");
    }
  }, [formik.isValid, formik.isValidating, formik.isSubmitting]);

  return (
    <div
      style={{
        display: "flex",
        justifyContent: "center",
        alignItems: "center",
        height: "100vh",
      }}
    >
      <div className="card px-4 py-5" style={{ width: 500 }}>
        <em
          className="fa fa-user-circle text-center d-block mb-3"
          style={{ fontSize: 100 }}
        />
        <h3 className="text-center mb-4">Inicio de Sesion</h3>
        <BaseInput
          id="userName"
          name="userName"
          label="Nombre de usuario"
          value={formik.values.userName}
          onChange={formik.handleChange}
          hasError={!!(formik.touched.userName && formik.errors.userName)}
          errorMessage={formik.errors.userName}
        />
        <BaseInput
          id="password"
          name="password"
          className="mt-2"
          label="Contraseña"
          type="password"
          value={formik.values.password}
          onChange={formik.handleChange}
          hasError={!!(formik.touched.password && formik.errors.password)}
          errorMessage={formik.errors.password}
        />
        <BaseButton
          className="mt-4 flex-grow-1"
          title="Ingresar"
          primary
          onClick={formik.handleSubmit}
        />
      </div>
    </div>
  );
};

export default LoginPage;
