import React, { useEffect, useMemo } from "react";
import { useFormik } from "formik";
import * as Yup from "yup";
import { toast } from "react-toastify";

// common components
import { BaseInput, BaseButton } from "../../../components/common";

// types
import { UserDto, UserFormDto } from "@/types";

interface Props {
  className?: string | undefined;
  user?: UserDto;
  onSaveClick: (user: UserDto) => void;
  onCancelClick: () => void;
}

// model validation schema
const validationSchema = Yup.object().shape({
  userName: Yup.string()
    .min(4, "El nombre de usuario debe tener al menos 6 caracteres.")
    .required("El nombre de usuario es requerido."),
  name: Yup.string().required("El nombre es requerido."),
  lastName: Yup.string().required("El apellido es requerido."),
  email: Yup.string()
    .email("El correo electrónico no es valido.")
    .required("El correo electrónico es requerido."),
  password: Yup.string()
    .min(6, "La contraseña debe tener al menos 6 caracteres.")
    .required("La contraseña es requerida."),
  confirmPassword: Yup.string()
    .oneOf([Yup.ref("password")], "Las contraseñas no coinciden.")
    .required("La confirmación de la contraseña es requerida."),
});

const UserManagement: React.FC<Props> = ({
  className,
  user,
  onSaveClick,
  onCancelClick,
}): React.ReactNode => {
  const isNewUser = useMemo(() => !user?.userId, [user?.userId]);

  const initialValues: UserFormDto = {
    userName: user?.userName || "",
    name: user?.name || "",
    lastName: user?.lastName || "",
    email: user?.email || "",
    isActive: user?.isActive || false,
    password: user?.password || "",
    confirmPassword: user?.password || "",
  };

  const onSubmitHandler = (values: UserFormDto) => {
    const payload: UserDto = {
      userId: user?.userId ?? null,
      userName: values.userName,
      name: values.name,
      lastName: values.lastName,
      email: values.email,
      isActive: values.isActive,
      password: values.password,
    } as UserDto;
    onSaveClick(payload);
  };

  const formik = useFormik({
    enableReinitialize: true,
    initialValues: initialValues,
    validationSchema: validationSchema,
    onSubmit: onSubmitHandler,
  });

  useEffect(() => {
    if (!formik.isValid && !formik.isValidating && formik.isSubmitting) {
      showToast("Favor completar los campos requeridos.");
    }
  }, [formik.isValid, formik.isValidating, formik.isSubmitting]);

  const showToast = (message: string) => {
    toast.error(message, {
      autoClose: 3000,
      hideProgressBar: true,
      toastId: "userManagement",
    });
  };
  return (
    <div
      className={`rounded p-3 mb-4 ${className ?? ""}`}
      style={{ backgroundColor: "#ecf0f1" }}
    >
      <div className="row">
        <div className="col-md-3 mb-3">
          <BaseInput
            id="userName"
            name="userName"
            label="Nombre de usuario:"
            value={formik.values.userName}
            onChange={formik.handleChange}
            disabled={!isNewUser}
            hasError={!!(formik.touched.userName && formik.errors.userName)}
            errorMessage={formik.errors.userName}
          />
        </div>
        <div className="col-md-3 mb-3">
          <BaseInput
            id="name"
            name="name"
            label="Nombre:"
            value={formik.values.name}
            onChange={formik.handleChange}
            hasError={!!(formik.touched.name && formik.errors.name)}
            errorMessage={formik.errors.name}
          />
        </div>
        <div className="col-md-3 mb-3">
          <BaseInput
            id="lastName"
            name="lastName"
            label="Apellido:"
            value={formik.values.lastName}
            onChange={formik.handleChange}
            hasError={!!(formik.touched.lastName && formik.errors.lastName)}
            errorMessage={formik.errors.lastName}
          />
        </div>
        <div className="col-md-3 mb-3">
          <BaseInput
            id="email"
            name="email"
            label="Correo electrónico:"
            value={formik.values.email}
            onChange={formik.handleChange}
            hasError={!!(formik.touched.email && formik.errors.email)}
            errorMessage={formik.errors.email}
          />
        </div>
        <div className="col-md-3 mb-3">
          <BaseInput
            id="password"
            name="password"
            type="password"
            label="Contraseña:"
            value={formik.values.password}
            onChange={formik.handleChange}
            hasError={!!(formik.touched.password && formik.errors.password)}
            errorMessage={formik.errors.password}
            disabled={!isNewUser}
          />
        </div>
        <div className="col-md-3 mb-3">
          <BaseInput
            id="confirmPassword"
            name="confirmPassword"
            type="password"
            label="Confirmar contraseña:"
            value={formik.values.confirmPassword}
            onChange={formik.handleChange}
            hasError={
              !!(
                formik.touched.confirmPassword && formik.errors.confirmPassword
              )
            }
            errorMessage={formik.errors.confirmPassword}
            disabled={!isNewUser}
          />
        </div>
        <div className="col-md-3">
          <div className="form-check mt-4 pt-3">
            <input
              className="form-check-input"
              type="checkbox"
              value={formik.values.isActive.toString()}
              checked={formik.values.isActive}
              onChange={formik.handleChange}
              id="isActive"
              name="isActive"
            />
            <label className="form-check-label" htmlFor="isActive">
              ¿Usuario activo?
            </label>
          </div>
        </div>
      </div>
      <div className="d-flex flex-row justify-content-end">
        <BaseButton
          className="me-2"
          title="Cancelar"
          onClick={() => {
            formik.resetForm();
            onCancelClick();
          }}
        />
        <BaseButton
          primary
          title="Guardar Cambios"
          onClick={formik.handleSubmit}
        />
      </div>
    </div>
  );
};

export default UserManagement;
