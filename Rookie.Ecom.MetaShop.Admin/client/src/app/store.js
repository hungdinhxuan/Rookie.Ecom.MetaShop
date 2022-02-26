import { configureStore } from '@reduxjs/toolkit'
import CategoryReducer from '../features/categorySlice'

export const store = configureStore({
  reducer: {
    category: CategoryReducer
  },
})