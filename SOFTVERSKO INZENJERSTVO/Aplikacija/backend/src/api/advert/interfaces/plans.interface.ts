interface IAdvertPlansSelectedItem {
  name: string;
  basic: boolean;
  standard: boolean;
  premium: boolean;
}

export interface IAdvertPlans {
  selected: IAdvertPlansSelectedItem[];
  basic: {
    description: string;
    consultation_number: number;
    consultation_time_minutes: string;
  };
  standard: {
    description: string;
    consultation_number: number;
    consultation_time_minutes: string;
  };
  premium: {
    description: string;
    consultation_number: number;
    consultation_time_minutes: string;
  };
}
