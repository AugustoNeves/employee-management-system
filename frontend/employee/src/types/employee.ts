export interface Employee {
  id: number;
  firstName: string;
  lastName: string;
  hireDate: string;
  phone: string;
  address: string;
  departmentId?: number;
  department: Department;
}

export interface Department {
  id: number;
  name: string;
}
