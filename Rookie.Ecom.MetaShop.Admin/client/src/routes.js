import { Navigate, useRoutes } from 'react-router-dom';
// layouts
import DashboardLayout from './layouts/dashboard';
import LogoOnlyLayout from './layouts/LogoOnlyLayout';
import DashboardApp from './pages/DashboardApp';
import Blog from './pages/Blog';
import User from './pages/User';
import Category from './pages/Category';
import Product from './pages/Product';
import NotFound from './pages/Page404';
import Forbidden from './pages/Page403';
import Welcome from './pages/welcome';
import CallbackPage from './components/CallbackPage';


// ----------------------------------------------------------------------



export default function Router() {
  return useRoutes([
    {
      path: '/dashboard',
      element: <DashboardLayout />, 
      children: [
        { path: 'app', element: <DashboardApp /> },
        { path: 'user', element: <User /> },
        { path: 'product', element: <Product /> },
        { path: 'category', element: <Category /> },
        { path: 'blog', element: <Blog /> }
      ]
    },
    { path: '/welcome', element: <Welcome /> },
    { path: '/callback', element: <CallbackPage /> },
    {
      path: '/',
      element: <LogoOnlyLayout />,
      children: [
        { path: '/', element: <Navigate to="/dashboard/app" /> },
        { path: '403', element: <Forbidden /> },
        { path: '404', element: <NotFound /> },
        { path: '*', element: <Navigate to="/404" /> },
      ]
    },
    { path: '*', element: <Navigate to="/404" replace /> }
  ]);
}
