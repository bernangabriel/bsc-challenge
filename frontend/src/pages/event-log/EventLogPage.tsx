import React, { useEffect, useState } from "react";

// service
import { getEvents as _getEvents } from "../../services/AppService";

// types
import { EventLogResultDto } from "@/types";

// child components
import { EventLogRow } from "./components";

interface Props {}

const EventLogPage: React.FC<Props> = (): React.ReactElement => {
  const [events, setEvents] = useState<EventLogResultDto[]>([]);

  useEffect(() => {
    const initialize = async () => {
      const response = await _getEvents();
      setEvents(response?.data);
    };
    initialize();
  }, []);

  return (
    <div>
      <h3>Eventos de Sistema</h3>
      {events.map((event, index) => (
        <EventLogRow key={index} event={event} />
      ))}
    </div>
  );
};

export default EventLogPage;
