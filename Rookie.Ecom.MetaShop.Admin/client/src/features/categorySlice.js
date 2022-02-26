import { createAsyncThunk, createSlice } from '@reduxjs/toolkit'
import axiosClient from '../utils/axiosClient'

const initialState = {
    categories: [],
    category: {},
  }


  export const getAllCategoriesAsync = createAsyncThunk(
    'categories/getAllCategories',
    async (values, { rejectWithValue }) => {
        try {
          const response = await axiosClient.get(`/category`)
          return response
        } catch (error) {
          return rejectWithValue(error.response.data)
        }
      }
    )

  

  export const counterSlice = createSlice({
    name: 'category',
    initialState,
    reducers: {
    },
    extraReducers: (builder) => {
        builder
        .addCase(getAllCategoriesAsync.fulfilled, (state, action) => {
            state.categories = action.payload.data
        })
    }
    
  })
  
  export default counterSlice.reducer