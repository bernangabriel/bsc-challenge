import React from "react";

// common components
import { BaseCard, BaseLabelValue } from "../../../components/common";

// types
import { EventLogResultDto } from "@/types";

interface Props {
  event: EventLogResultDto;
}

const EventLogRow: React.FC<Props> = ({ event }): React.ReactNode => {
  return (
    <BaseCard className="mb-3">
      <div className="row px-3 py-2">
        <div className="col">
          <BaseLabelValue
            title="Usuario"
            value={event.userName}
            valueClassName=""
          />
        </div>
        <div className="col">
          <BaseLabelValue title="Nombre de usuario" value={event.fullName} />
        </div>
        <div className="col">
          <BaseLabelValue title="Evento" value={event.eventTypeName} />
        </div>
        <div className="col">
          <BaseLabelValue title="Payload" value={event.payload} />
        </div>
        <div className="col col-sm col-6 mt-3 mt-md-0">
          <BaseLabelValue title="Fecha del evento" value={event.eventDate} />
        </div>
      </div>
    </BaseCard>
  );
};

export default EventLogRow;
