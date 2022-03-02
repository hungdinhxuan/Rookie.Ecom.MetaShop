import { useRef, useState } from 'react';

// material
import { Menu, MenuItem, IconButton, ListItemIcon, ListItemText } from '@mui/material';
// component
import Iconify from '../../../components/Iconify';

import { useDispatch } from "react-redux";
import {deleteCategoryAsync, setCategory} from "../../../features/categorySlice";

// ----------------------------------------------------------------------

import {swalWithBootstrapButtons} from "src/utils/sweetalert2";
import Swal from "sweetalert2";
import UpdateCategory from "src/sections/@dashboard/categories/UpdateCategory";
import { memo } from 'react';

function CategoryMoreMenu({category}) {
  
  const ref = useRef(null);
  const dispatch = useDispatch();
  const [isDeleteDialogConfirm, setIsDeleteDialogConfirm] = useState(false);
  const [isUpdateDialogOpen, setIsUpdateDialogOpen] = useState(false);

  const handleShowDeleteConfirm = () => {
    setIsDeleteDialogConfirm(false);
    swalWithBootstrapButtons.fire({
      title: 'Are you sure?',
      text: "You won't be able to revert this!",
      icon: 'warning',
      showCancelButton: true,
      confirmButtonText: 'Yes, delete it!',
      cancelButtonText: 'No, cancel!',
      reverseButtons: true
    }).then((result) => {
      if (result.isConfirmed) {
        dispatch(deleteCategoryAsync({id: category.id}));
        
      } else if (
        /* Read more about handling dismissals below */
        result.dismiss === Swal.DismissReason.cancel
      ) {
        swalWithBootstrapButtons.fire(
          'Cancelled',
          'Your imaginary file is safe :)',
          'error'
        )
      }
    })
  };

  

  return (
    <>
      <IconButton ref={ref} onClick={() => {
        setIsDeleteDialogConfirm(true)
        dispatch(setCategory(category));
      }}>
        <Iconify icon="eva:more-vertical-fill" width={20} height={20} />
      </IconButton>

      <Menu
        open={isDeleteDialogConfirm}
        anchorEl={ref.current}
        onClose={() => {
          setIsDeleteDialogConfirm(false)
        }}
        PaperProps={{
          sx: { width: 200, maxWidth: '100%' }
        }}
        anchorOrigin={{ vertical: 'top', horizontal: 'right' }}
        transformOrigin={{ vertical: 'top', horizontal: 'right' }}
      >
        <MenuItem sx={{ color: 'text.secondary' }}>
          <ListItemIcon>
            <Iconify icon="eva:trash-2-outline" width={24} height={24} />
          </ListItemIcon>
          <ListItemText primary="Delete" primaryTypographyProps={{ variant: 'body2' }} onClick={handleShowDeleteConfirm}/>
        </MenuItem>

        <MenuItem sx={{ color: 'text.secondary' }}>
          <ListItemIcon>
            <Iconify icon="eva:edit-fill" width={24} height={24} />
          </ListItemIcon>
          <ListItemText primary="Edit" primaryTypographyProps={{ variant: 'body2' }} onClick={() => setIsUpdateDialogOpen(true)}/>
        </MenuItem>
      </Menu>
      {
        isUpdateDialogOpen && <UpdateCategory open={isUpdateDialogOpen} setOpen={setIsUpdateDialogOpen} />
      }

    </>
  );
}

export default memo(CategoryMoreMenu);