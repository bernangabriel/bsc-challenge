import React, { useMemo } from "react";

// common components
import { BaseCard, BaseLabelValue } from "../../../components/common";

// types
import { UserDto } from "@/types";

interface Props {
  user: UserDto;
  onEditClick: () => void;
  onDeleteClick: () => void;
}

const UserRow: React.FC<Props> = ({
  user,
  onEditClick,
  onDeleteClick,
}): React.ReactNode => {
  const userStatus = useMemo(
    () => (user.isActive ? "Activo" : "Inactivo"),
    [user.isActive]
  );
  return (
    <BaseCard className="mb-3">
      <div className="row px-3 py-2">
        <div className="col">
          <BaseLabelValue
            title="Usuario"
            value={user.userName}
            valueClassName="fw-"
          />
        </div>
        <div className="col">
          <BaseLabelValue title="Nombre" value={user.name} />
        </div>
        <div className="col col-sm col-6 mt-3 mt-md-0">
          <BaseLabelValue title="Apellido" value={user.lastName} />
        </div>
        <div className="col col-sm col-6 mt-3 mt-md-0">
          <BaseLabelValue title="Email" value={user.email} />
        </div>
        <div className="col col-sm col-6 mt-3 mt-md-0">
          <BaseLabelValue
            title="Fecha de Creación"
            value={user.createdDate.toLocaleString()}
          />
        </div>
        <div className="col col-sm col-6 mt-3 mt-md-0">
          <BaseLabelValue
            title=""
            value={userStatus}
            valueClassName={`d-flex align-items-center justify-content-center w-50 badge rounded-pill ${
              user.isActive ? "bg-success" : "bg-danger"
            }`}
          />
        </div>
        <div className="col-1 mt-md-0 d-flex align-items-center justify-content-center">
          <em className="fa fa-edit fs-4 me-3" onClick={onEditClick} />
          <em className="fa fa-trash fs-4" onClick={onDeleteClick} />
        </div>
      </div>
    </BaseCard>
  );
};

export default UserRow;
