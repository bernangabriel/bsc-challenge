export default interface SignInResultDto {
    isValid: boolean;
    message?: string;
    accessToken: string;
}