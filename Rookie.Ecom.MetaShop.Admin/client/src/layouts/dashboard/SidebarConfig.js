// component
import Iconify from '../../components/Iconify';

// ----------------------------------------------------------------------

const getIcon = (name) => <Iconify icon={name} width={22} height={22} />;

const sidebarConfig = [
  {
    title: 'dashboard',
    path: '/dashboard/app',
    icon: getIcon('eva:pie-chart-2-fill')
  },
  {
    title: 'user',
    path: '/dashboard/user',
    icon: getIcon('eva:people-fill')
  },

  {
    title: 'category',
    path: '/dashboard/category',
    icon: getIcon('bx:category-alt')
  },
  {
    title: 'product',
    path: '/dashboard/product',
    icon: getIcon('eva:shopping-bag-fill')
  },
  {
    title: 'blog',
    path: '/dashboard/blog',
    icon: getIcon('eva:file-text-fill')
  }
];

export default sidebarConfig;
