import axios, { AxiosRequestConfig, AxiosResponse } from "axios";
import { PatientDTO } from "../models/patientDTO";
import { API_BASE_URL } from "../config";
import { LoginUserDTO } from "../models/loginUserDTO";
import { AuthDTO } from "../models/authDTO";
import { RegisterUserDTO } from "../models/registerUserDTO";

const apiConnector = {
  getPatients: async (token: string): Promise<PatientDTO[]> => {
    try {
      const response: AxiosResponse<PatientDTO[]> = await axios.get(
        `${API_BASE_URL}/patient`,
        {
          headers: {
            Authorization: `Bearer ${token}`,
            ContentType: "application/json",
          },
        }
      );

      const patients = response.data.map((patient) => ({
        ...patient,
      }));

      return patients;
    } catch (error) {
      console.log("Error fetching patients:", error);
      throw error;
    }
  },

  createPatient: async (patient: PatientDTO, token: string): Promise<void> => {
    try {
      const config: AxiosRequestConfig = {
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${token}`,
        },
      };
      await axios.post<number>(`${API_BASE_URL}/patient`, patient, config);
    } catch (error) {
      console.log(error);
      throw error;
    }
  },

  editPatient: async (patient: PatientDTO, token: string): Promise<void> => {
    try {
      const config: AxiosRequestConfig = {
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${token}`,
        },
      };
      await axios.put<number>(
        `${API_BASE_URL}/patient/${patient.id}`,
        patient,
        config
      );
    } catch (error) {
      console.log(error);
      throw error;
    }
  },

  deletePatient: async (patientId: number, token: string): Promise<void> => {
    try {
      const config: AxiosRequestConfig = {
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${token}`,
        },
      };
      await axios.delete<number>(
        `${API_BASE_URL}/patient/${patientId}`,
        config
      );
    } catch (error) {
      console.log(error);
      throw error;
    }
  },

  getPatientById: async (
    patientId: number,
    token: string
  ): Promise<PatientDTO | undefined> => {
    try {
      const config: AxiosRequestConfig = {
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${token}`,
        },
      };
      const response = await axios.get<PatientDTO>(
        `${API_BASE_URL}/patient/${patientId}`,
        config
      );
      return response.data;
    } catch (error) {
      console.log(error);
      throw error;
    }
  },

  loginUser: async (user: LoginUserDTO): Promise<AuthDTO> => {
    try {
      const response: AxiosResponse<AuthDTO> = await axios.put(
        `${API_BASE_URL}/user`,
        user
      );
      const userResponse = response.data;
      sessionStorage.setItem("username", userResponse.userName);
      sessionStorage.setItem("jwttoken", userResponse.token);
      return userResponse;
    } catch (error) {
      console.log(error);
      throw error;
    }
  },

  createUser: async (user: RegisterUserDTO): Promise<AuthDTO> => {
    try {
      const response: AxiosResponse<AuthDTO> = await axios.post(
        `${API_BASE_URL}/user`,
        user
      );
      const userResponse = response.data;
      sessionStorage.setItem("username", userResponse.userName);
      sessionStorage.setItem("jwttoken", userResponse.token);
      return userResponse;
    } catch (error) {
      console.log(error);
      throw error;
    }
  },
};

export default apiConnector;
