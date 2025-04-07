import { useEffect, useState } from 'react';
import { Link, redirect, useParams } from 'react-router-dom';
import {
  getEmployeeById,
  getDepartments,
  updateEmployee,
} from '../services/api';
import { Employee, Department } from '../types/employee';
import {
  Box,
  Typography,
  CircularProgress,
  Paper,
  Avatar,
  FormControl,
  InputLabel,
  Select,
  MenuItem,
  Button,
  SelectChangeEvent,
} from '@mui/material';
import Grid from '@mui/material/Grid2';
import { calculateEmploymentDuration } from '../utils/dateUtils';
import { deepOrange } from '@mui/material/colors';
import HomeIcon from '@mui/icons-material/Home';
import IconButton from '@mui/material/IconButton';

function EmployeePage() {
  const { id } = useParams();
  const [employee, setEmployee] = useState<Employee | null>(null);
  const [departments, setDepartments] = useState<Department[]>([]);
  const [loading, setLoading] = useState(true);
  const [saving, setSaving] = useState(false);

  useEffect(() => {
    const fetchData = async () => {
      setLoading(true);
      try {
        const empData = await getEmployeeById(Number(id));
        const deptData = await getDepartments();
        setEmployee(empData);
        setDepartments(deptData);
      } catch (error) {
        console.error('Error fetching employee data:', error);
      } finally {
        setLoading(false);
      }
    };
    fetchData();
  }, [id]);

  const handleDepartmentChange = (e: SelectChangeEvent<number>) => {
    if (!employee) return;
    setEmployee({
      ...employee,
      department: departments.find((d) => d.id === Number(e.target.value))!,
    });
  };
  const handleSave = async () => {
    if (!employee) return;
    setSaving(true);
    try {
      await updateEmployee({
        ...employee,
        departmentId: employee.department.id,
      });

      alert('Employee updated successfully!');
    } catch (error) {
      console.error('Error updating employee:', error);
    } finally {
      setSaving(false);
      redirect('/');
    }
  };

  if (loading)
    return (
      <CircularProgress sx={{ display: 'block', margin: 'auto', mt: 5 }} />
    );

  return (
    <Box
      sx={{
        maxWidth: 1024,
        margin: 'auto',
        mt: 4,
        p: 3,
        boxShadow: 3,
        borderRadius: 2,
        bgcolor: 'white',
      }}
    >
      <Typography variant="h5" sx={{ mb: 3 }}>
        {' '}
        <IconButton component={Link} to={'/'}>
          <HomeIcon />
        </IconButton>{' '}
        Employee Details
      </Typography>
      {employee ? (
        <Paper elevation={3} sx={{ p: 3 }}>
          <Grid container spacing={2}>
            <Grid size={{ xs: 4 }}>
              <Avatar
                sx={{ bgcolor: deepOrange[500], width: '100%', height: '100%' }}
                variant="square"
              >
                {employee.firstName[0]}
                {employee.lastName[0]}
              </Avatar>
            </Grid>
            <Grid size={{ xs: 6 }}>
              <Typography variant="h4" sx={{ mb: 2 }}>
                {employee.firstName} {employee.lastName}
              </Typography>
              <Typography variant="h6" sx={{ mb: 2 }}>
                Employee ID: {employee.id}
              </Typography>
              <Typography variant="h6" sx={{ mb: 2 }}>
                Department: {employee.department?.name}
              </Typography>
              <Typography variant="h6" sx={{ mb: 2 }}>
                Telephone: {employee.phone}
              </Typography>
              <Typography variant="h6" sx={{ mb: 2 }}>
                Address: {employee.address}
              </Typography>

              <Box display={'flex'}>
                <FormControl fullWidth sx={{ mb: 2, mt: 2 }}>
                  <InputLabel>Department</InputLabel>
                  <Select
                    value={employee.department.id}
                    onChange={handleDepartmentChange}
                    label="Department"
                  >
                    {departments.map((dept) => (
                      <MenuItem key={dept.id} value={dept.id}>
                        {dept.name}
                      </MenuItem>
                    ))}
                  </Select>
                </FormControl>
                <Button
                  onClick={handleSave}
                  variant="contained"
                  color="success"
                  sx={{ m: 1, marginY: 2 }}
                  disabled={saving}
                >
                  {saving ? 'Saving...' : 'Save'}
                </Button>
              </Box>
            </Grid>

            <Grid size={{ xs: 2 }}>
              <Typography variant="h6">Hire Date</Typography>
              <Typography variant="body1">
                {new Intl.DateTimeFormat('en-US', {
                  month: 'long',
                  day: 'numeric',
                  year: 'numeric',
                }).format(new Date(employee.hireDate))}
              </Typography>
              <Typography>
                {calculateEmploymentDuration(employee.hireDate)}
              </Typography>
            </Grid>
          </Grid>
        </Paper>
      ) : (
        <Typography color="error">Employee not found.</Typography>
      )}
    </Box>
  );
}

export default EmployeePage;
