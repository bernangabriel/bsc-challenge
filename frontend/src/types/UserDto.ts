export default interface UserDto {
  userId?: string;
  userName: string;
  name: string;
  lastName: string;
  email: string;
  createdDate: Date;
  isActive: boolean;
  password?: string;
}
