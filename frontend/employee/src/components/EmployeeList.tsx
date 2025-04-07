import {
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  Paper,
  Button,
  Avatar,
} from '@mui/material';
import { deepOrange } from '@mui/material/colors';
import { Link } from 'react-router-dom';
import { calculateEmploymentDuration } from '../utils/dateUtils';
import { Employee } from '../types/employee';

interface EmployeeListProps {
  employees: Employee[];
  deleteEmployeeFromState: (id: number) => Promise<void>;
}

export function EmployeeList({ employees, deleteEmployeeFromState }: EmployeeListProps) {

  const handleDelete = async (id: number) => {
    try {
      await deleteEmployeeFromState(id); 
    } catch (error) {
      console.error('Error deleting employee:', error);
    }
  };

  return (
    <TableContainer component={Paper}>
      <Table>
        <TableHead>
          <TableRow>
            <TableCell></TableCell>
            <TableCell>
              <strong>Name</strong>
            </TableCell>
            <TableCell>
              <strong>Department</strong>
            </TableCell>
            <TableCell>
              <strong>Hire Date</strong>
            </TableCell>
            <TableCell>
              <strong>Actions</strong>
            </TableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {employees.map((employee) => (
            <TableRow key={employee.id}>
              <TableCell>
                <Avatar sx={{ bgcolor: deepOrange[500] }}>
                  {employee.firstName[0] + employee.lastName[0]}
                </Avatar>
              </TableCell>
              <TableCell>
                {employee.firstName} {employee.lastName}
              </TableCell>
              <TableCell>{employee.department?.name}</TableCell>
              <TableCell>
                {new Intl.DateTimeFormat('en-US', {
                  month: 'long',
                  day: 'numeric',
                  year: 'numeric',
                }).format(new Date(employee.hireDate)) +
                  ' (' +
                  calculateEmploymentDuration(employee.hireDate) +
                  ')'}
              </TableCell>
              <TableCell>
                <Button
                  component={Link}
                  to={`/employee/${employee.id}`}
                  variant="contained"
                  color="primary"
                  size="small"
                  sx={{ mr: 1 }}
                >
                  View
                </Button>
                <Button
                  variant="contained"
                  color="error"
                  size="small"
                  onClick={() => handleDelete(employee.id)}
                >
                  Delete
                </Button>
              </TableCell>
            </TableRow>
          ))}
        </TableBody>
      </Table>
    </TableContainer>
  );
}
