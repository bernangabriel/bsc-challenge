import React, { useState, useEffect } from "react";
import { toast } from "react-toastify";
import { confirmAlert } from "react-confirm-alert";

// types
import { RemoveUserInputDto, UserDto } from "@/types";

// services
import {
  getUsers as _getUsers,
  saveUser as _saveUser,
  removeUser as _removeUser,
} from "../../services/AppService";

// child components
import { UserRow, UserManagement } from "./components";

interface Props {}

const UserManagementPage: React.FC<Props> = (): React.ReactElement => {
  const [users, setUsers] = useState<UserDto[]>([]);
  const [selectedUser, setSelectedUser] = useState<UserDto | undefined>();

  useEffect(() => {
    getUsers();
  }, []);

  const getUsers = async () => {
    const result = (await _getUsers()).data;
    setUsers(result);
  };

  const onEditClickHandler = (value: UserDto) => {
    if (value?.userId) {
      const mappedUser = {
        ...value,
        password: "fake-password",
        confirmPassword: "fake-password",
      } as UserDto;
      setSelectedUser(mappedUser);
    }
  };

  const removeUser = async (value: RemoveUserInputDto) => {
    if (value?.userId) {
      const response = await _removeUser(value);
      if (response?.data) {
        toast.success("El usuario ha sido eliminado exitosamente", {
          autoClose: 3000,
          toastId: "removeUser",
        });
        await getUsers();
      }
    }
  };

  const onConfirmDeleteUserHandler = (value: UserDto) => {
    if (value?.userId) {
      confirmAlert({
        title: "Eliminar Usuario",
        message: "¿Está seguro que desea eliminar este usuario?",
        buttons: [
          {
            label: "Sí",
            className: "btn btn-danger",
            onClick: () =>
              removeUser({ userId: value.userId } as RemoveUserInputDto),
          },
          {
            label: "No",
            onClick: () => {},
          },
        ],
      });
    }
  };

  const onSaveClickHandler = async (user: UserDto) => {
    const response = await _saveUser(user);
    if (response?.data) {
      toast.success("El usuario ha sido guardado exitosamente.", {
        toastId: "saveUser",
      });

      if (!user.userId) {
        console.log("user", user);
        setSelectedUser(undefined);
      }

      await getUsers();
    }
  };

  return (
    <div>
      <h3>Gestion de Usuarios</h3>
      <UserManagement
        className="mx-0"
        user={selectedUser}
        onCancelClick={() => setSelectedUser(undefined)}
        onSaveClick={(value) => onSaveClickHandler(value)}
      />
      {users.map((user) => (
        <UserRow
          key={user.userId}
          user={user}
          onEditClick={() => onEditClickHandler(user)}
          onDeleteClick={() => onConfirmDeleteUserHandler(user)}
        />
      ))}
    </div>
  );
};

export default UserManagementPage;
