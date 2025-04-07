import axios from 'axios';
import { Employee } from '../types/employee';

const api = axios.create({
  baseURL: import.meta.env.VITE_API_BASE_URL,
  headers: {
    "Content-Type": "application/json",
    "X-API-KEY": import.meta.env.VITE_API_KEY
  }
});

export const getEmployees = async () => {
  const response = await api.get('/Employee');
  return response.data;
};

export const getEmployeeById = async (id: number) => {
  const response = await api.get(`/Employee/${id}`);
  return response.data;
};

export const getDepartments = async () => {
  const response = await api.get('/Department');
  return response.data;
};

export const addEmployee = async (newEmployee: Employee) => {
  const response = await api.post('/Employee', newEmployee);
  return response.data;
};

export const deleteEmployee = async (id: number) => {
  const response = await api.delete(`/Employee/${id}`);
  return response.data;
};

export const updateEmployee = async (updatedEmployee: Employee) => {
  const response = await api.put(`/Employee/${updatedEmployee.id}`, updatedEmployee)
  return response.data;
}

export default api;
