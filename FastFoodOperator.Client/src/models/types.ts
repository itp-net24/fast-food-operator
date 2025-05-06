export interface BaseProduct {
  id: number;
  name: string;
  description: string | null;
  basePrice: number;
  imageUrl: string | null;
  tags: Tag[];
}

export interface Product extends BaseProduct{
  defaultVariant?: Variant | null;
  variants: Variant[];

  ingredients: Ingredient[];
}

export interface Combo extends BaseProduct {
  mainComboProductId: number | null;
  mainComboProduct: ComboProduct | null;

  comboProducts: ComboProduct[];
  comboGroups: ComboGroup[];
}


export interface ComboGroup {
  id: number;
  name: string;
  defaultComboProductId: number | null;
  defaultComboProduct: ComboProduct | null;
  comboProducts: ComboProduct[];
}

export interface ComboProduct {
  __uid: number,
  id: number;
  product: Product;
  defaultProductVariantId: number | null;
  defaultProductVariant: Variant | null;
}


export interface Tag {
  id: number;
  name: string;
  tax: number;
}

export interface Variant {
  id: number;
  name: string;
  priceModifier: number;
}

export interface Ingredient {
  id: number;
  name: string;
  priceModifier: number;
}


export interface TaxSummary {
  taxRate: number; // e.g. 0.25 for 25%
  priceSummary: PriceSummary;
}

export interface PriceSummary {
  total: number,
  net: number,
  gross: number,
}


export interface CartContainer {
  id: number;
  type: string;
  imageUrl: string | null;
  name: string;
  tags: Tag[];
  price: number;
  quantity: number;
  products: CartItem[];
}

export interface CartItem {
  __uid: number;
  id: number;
  name: string;
  tax: number;
  basePrice: number;
  variant?: Variant | null;
  ingredients?: Ingredient[] | null;
}
