export default interface EventLogResultDto {
  id: number;
  userName: string;
  fullName: string;
  eventTypeName: string;
  eventDate: string;
  payload: string;
}
