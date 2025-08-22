export interface IPayPalCreateOrderLinks {
  href: string;
  rel: 'self' | 'approve' | 'update' | 'capture';
  method: 'GET' | 'POST' | 'PATCH' | 'PUT' | 'DELETE';
}

interface PayPalName {
  given_name: string;
  surname: string;
}

interface PayPalEmailAddress {
  email_address: string;
}

interface PayPalAccount {
  account_id: string;
  name: PayPalName;
  email_address: PayPalEmailAddress;
}

interface ShippingAddress {
  address_line_1: string;
  address_line_2: string;
  admin_area_2: string;
  admin_area_1: string;
  postal_code: string;
  country_code: string;
}

interface CaptureAmount {
  currency_code: string;
  value: string;
}

interface SellerProtection {
  status: string;
  dispute_categories: string[];
}

interface SellerReceivableBreakdown {
  gross_amount: CaptureAmount;
  paypal_fee: CaptureAmount;
  net_amount: CaptureAmount;
}

interface PaymentCapture {
  id: string;
  status: string;
  amount: CaptureAmount;
  seller_protection: SellerProtection;
  final_capture: boolean;
  disbursement_mode: string;
  seller_receivable_breakdown: SellerReceivableBreakdown;
  create_time: string;
  update_time: string;
  links: { href: string; rel: string; method: string }[];
}

interface Payments {
  captures: PaymentCapture[];
}

interface PurchaseUnit {
  reference_id: string;
  shipping: { address: ShippingAddress };
  payments: Payments;
}

interface Payer {
  name: PayPalName;
  email_address: string;
  payer_id: string;
}

export interface IPayPalTransaction {
  id: string;
  status: string;
  payment_source: { paypal: PayPalAccount };
  purchase_units: PurchaseUnit[];
  payer: Payer;
  links: { href: string; rel: string; method: string }[];
}
