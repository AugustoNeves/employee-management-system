export function calculateEmploymentDuration(hireDate: string): string {
  const hire = new Date(hireDate);
  const now = new Date();

  const years = now.getFullYear() - hire.getFullYear();
  const months = now.getMonth() - hire.getMonth();
  const days = now.getDate() - hire.getDate();

  let adjustedYears = years;
  let adjustedMonths = months;
  let adjustedDays = days;

  if (adjustedDays < 0) {
    adjustedMonths -= 1;
    adjustedDays += new Date(now.getFullYear(), now.getMonth(), 0).getDate();
  }
  if (adjustedMonths < 0) {
    adjustedYears -= 1;
    adjustedMonths += 12;
  }

  return `${adjustedYears}y - ${adjustedMonths}m - ${adjustedDays}d`;
}
