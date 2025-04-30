export interface BaseProduct {
  id: number;
  name: string;
  description: string | null;
  basePrice: number;
  imageUrl: string | null;
}

export interface Product extends BaseProduct{
  defaultVariant?: ProductVariant | null;
  variants: ProductVariant[];
  ingredients: Ingredient[];
}

export interface Ingredient {
  id: number;
  name: string;
  priceModifier: number;
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
  comboProducts: ComboProduct[];
}

export interface ComboProduct {
  id: number;
  product: Product;
  defaultProductVariantId: number | null;
  defaultProductVariant: ProductVariant | null;
}

export interface ProductVariant {
  id: number;
  name: string;
  priceModifier: number;
}



// ToCart interfaces
export interface CartItem {
  id: number;
  type: string;
  name: string;
  price: number;
  quantity: number;
  products: ProductToCart[];
}

export interface ProductToCart {
  id: number;
  name: string;
  variant?: VariantToCart | null;
  ingredients?: IngredientToCart[] | null;
}

export interface VariantToCart {
  id: number;
  name: string;
}

export interface IngredientToCart {
  id: number;
  name: string;
}
