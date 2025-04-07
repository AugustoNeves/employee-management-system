import { useEffect, useState } from 'react';
import {
  Modal,
  Box,
  TextField,
  Button,
  Typography,
  FormControl,
  Select,
  InputLabel,
  MenuItem,
  SelectChangeEvent,
} from '@mui/material';
import { Employee, Department } from '../types/employee';
import { getDepartments } from '../services/api';

interface Props {
  open: boolean;
  onClose: () => void;
  addEmployeeToState: (employee: Employee) => Promise<void>;
}

export function EmployeeFormModal({
  open,
  onClose,
  addEmployeeToState,
}: Props) {
  const [employee, setEmployee] = useState({
    id: 0,
    firstName: '',
    lastName: '',
    departmentId: 0,
    department: { id: 0, name: '' },
    hireDate: '',
    phone: '',
    address: '',
  });
  const [departments, setDepartments] = useState<Department[]>([]);

  const handleAddEmployee = async (newEmployee: Employee) => {
    try {
      await addEmployeeToState(newEmployee);
    } catch (error) {
      console.error('Error creating employee:', error);
    }
  };

  useEffect(() => {
    if (open) {
      const fetchDepartments = async () => {
        try {
          const data = await getDepartments();
          setDepartments(data);
        } catch (error) {
          console.error('Error fetching departments:', error);
        }
      };
      fetchDepartments();
    }
  }, [open]);

  const handleInputChange = (
    e: React.ChangeEvent<{ name?: string; value: unknown }>
  ) => {
    setEmployee((prev) => ({
      ...prev,
      [e.target.name as string]: e.target.value,
    }));
  };

  const handleSelectChange = (e: SelectChangeEvent) => {
    const selectedDepartment = departments.find(
      (dept) => dept.id === Number(e.target.value)
    );

    setEmployee((prev) => ({
      ...prev,
      departmentId: Number(e.target.value),
      department: selectedDepartment || { id: 0, name: '' },
    }));
  };

  const handleSubmit = () => {
    handleAddEmployee(employee);
    onClose();
  };

  return (
    <Modal open={open} onClose={onClose}>
      <Box
        sx={{
          position: 'absolute',
          top: '50%',
          left: '50%',
          transform: 'translate(-50%, -50%)',
          bgcolor: 'white',
          p: 3,
          borderRadius: 2,
        }}
      >
        <Typography variant="h6" sx={{ mb: 2 }}>
          New Employee
        </Typography>
        <TextField
          fullWidth
          label="First Name"
          name="firstName"
          onChange={handleInputChange}
          sx={{ mb: 2 }}
        />
        <TextField
          fullWidth
          label="Last Name"
          name="lastName"
          onChange={handleInputChange}
          sx={{ mb: 2 }}
        />
        <TextField
          fullWidth
          label="Phone Number"
          name="phone"
          onChange={handleInputChange}
          sx={{ mb: 2 }}
        />

        <TextField
          sx={{ mb: 2 }}
          label="Hire Date"
          name="hireDate"
          type="date"
          value={employee.hireDate}
          onChange={handleInputChange}
          fullWidth
          required
          margin="normal"
          slotProps={{
            inputLabel: {
              shrink: true,
            },
          }}
        />

        <FormControl fullWidth sx={{ mb: 2 }}>
          <InputLabel id="department-select-label">Department</InputLabel>
          <Select
            name="departmentId"
            value={
              employee.departmentId > 0 ? employee.departmentId.toString() : ''
            }
            id="department-id"
            onChange={handleSelectChange}
            label="Department"
          >
            {departments.length === 0 ? (
              <MenuItem value="" disabled>
                No Departments Available
              </MenuItem>
            ) : (
              departments.map((dept) => (
                <MenuItem key={dept.id} value={dept.id}>
                  {dept.name}
                </MenuItem>
              ))
            )}
          </Select>
        </FormControl>
        <TextField
          fullWidth
          label="Address"
          name="address"
          onChange={handleInputChange}
          sx={{ mb: 2 }}
        />
        <Button
          variant="contained"
          color="primary"
          onClick={handleSubmit}
          sx={{ mr: 1 }}
        >
          Submit
        </Button>
        <Button variant="outlined" color="secondary" onClick={onClose}>
          Cancel
        </Button>
      </Box>
    </Modal>
  );
}
