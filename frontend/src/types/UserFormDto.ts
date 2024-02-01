export default interface UserFormDto {
  userName: string;
  name: string;
  lastName: string;
  email: string;
  isActive: boolean;
  password?: string;
  confirmPassword?: string;
}
