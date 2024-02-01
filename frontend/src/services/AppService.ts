import { AxiosResponse } from "axios";

import appClient from "../config/service";

// types
import {
  UserDto,
  SignInResultDto,
  RemoveUserInputDto,
  UserLoginForm,
  UserProfileInfoDto,
  EventLogResultDto,
} from "../types";

const baseApiName = "api/main";

export const signIn = async (
  payload: UserLoginForm
): Promise<AxiosResponse<SignInResultDto>> => {
  return appClient.post("api/account/signin", payload);
};

export const getProfileInformation = async (): Promise<
  AxiosResponse<UserProfileInfoDto>
> => {
  return appClient.get(`api/account/user-profile-information`);
};

export const getUsers = async (): Promise<AxiosResponse<UserDto[]>> => {
  return appClient.get(`${baseApiName}/users`);
};

export const saveUser = async (
  user: UserDto
): Promise<AxiosResponse<boolean>> => {
  return appClient.post(`${baseApiName}/save-user`, user);
};

export const removeUser = async (
  user: RemoveUserInputDto
): Promise<AxiosResponse<boolean>> => {
  return appClient.delete(`${baseApiName}/remove-user`, {
    data: user,
  });
};

export const getEvents = async (): Promise<
  AxiosResponse<EventLogResultDto[]>
> => {
  return appClient.get(`${baseApiName}/event-logs`);
};
