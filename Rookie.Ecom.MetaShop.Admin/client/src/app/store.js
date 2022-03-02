import { configureStore } from '@reduxjs/toolkit'
import CategoryReducer from '../features/categorySlice';
import ProductReducer from '../features/productSlice';
import { getDefaultMiddleware } from '@reduxjs/toolkit';
const customizedMiddleware = getDefaultMiddleware({
  serializableCheck: false
})
export const store = configureStore({
  reducer: {
    category: CategoryReducer,
    product: ProductReducer
  },
  middleware: customizedMiddleware
})