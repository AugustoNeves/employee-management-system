import { useState } from 'react';
import { Button, Typography, Box } from '@mui/material';
import { EmployeeFormModal } from '../components/EmployeeFormModal';
import { useEmployees } from '../hooks/useEmployees';
import { EmployeeList } from '../components/EmployeeList';

function Home() {
  const { employees, loading, deleteEmployeeFromState, addEmployeeToState } =
    useEmployees();
  const [open, setOpen] = useState(false);



  return (
    <Box sx={{ maxWidth: 1024, margin: 'auto', mt: 4, p: 2 }}>
      <Box
        sx={{
          display: 'flex',
          justifyContent: 'space-between',
          alignItems: 'center',
          mb: 2,
        }}
      >
        <Typography variant="h5" sx={{ mb: 2 }}>
          Employee List
        </Typography>
        <Button
          variant="contained"
          color="primary"
          sx={{ mb: 2 }}
          onClick={() => setOpen(true)}
        >
          New Employee
        </Button>
      </Box>

      {loading ? (
        <Typography>Loading...</Typography>
      ) : (
        <EmployeeList
          employees={employees}
          deleteEmployeeFromState={deleteEmployeeFromState}
        />
      )}

      <EmployeeFormModal
        open={open}
        onClose={() => setOpen(false)}
        addEmployeeToState={addEmployeeToState}
      />
    </Box>
  );
}

export default Home;
