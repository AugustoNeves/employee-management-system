import { useEffect, useState } from 'react';
import { addEmployee, getEmployees, deleteEmployee } from '../services/api';
import { Employee } from '../types/employee';

export function useEmployees() {
  const [employees, setEmployees] = useState<Employee[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const fetchEmployees = async () => {
      try {
        const data = await getEmployees();
        setEmployees(data);
      } catch (error) {
        console.error('Error fetching employees:', error);
      } finally {
        setLoading(false);
      }
    };
    fetchEmployees();
  }, []);

  const addEmployeeToState = async (newEmployee: Employee) => {
    try {
      const createdEmployeeId = await addEmployee(newEmployee);
      newEmployee.id = createdEmployeeId
      setEmployees((prev) => [...prev, newEmployee]);
    } catch (error) {
      console.error('Error adding employee:', error);
      throw error;
    }
  };

  const deleteEmployeeFromState = async (id: number) => {
    try {
      await deleteEmployee(id);
      setEmployees((prev) => prev.filter((employee) => employee.id !== id));
    } catch (error) {
      console.error('Error deleting employee:', error);
      throw error;
    }
  };

  return {
    employees,
    loading,
    setEmployees,
    addEmployeeToState,
    deleteEmployeeFromState,
  };
}
