import { motion } from 'framer-motion';
import { Link as RouterLink } from 'react-router-dom';
// material
import { styled } from '@mui/material/styles';
import { Box, Button, Typography, Container } from '@mui/material';
// components
import { MotionContainer, varBounceIn } from '../components/animate';
import Page from '../components/Page';
import userManager from 'src/utils/userManager';

// ----------------------------------------------------------------------

const RootStyle = styled(Page)(({ theme }) => ({
  display: 'flex',
  minHeight: '100%',
  alignItems: 'center',
  paddingTop: theme.spacing(15),
  paddingBottom: theme.spacing(10)
}));

// ----------------------------------------------------------------------

export default function Page403() {
  return (
    <RootStyle title="403 Forbidden">
      <Container>
        <MotionContainer initial="initial" open>
          <Box sx={{ maxWidth: 480, margin: 'auto', textAlign: 'center' }}>
            <motion.div variants={varBounceIn}>
              <Typography variant="h3" paragraph>
                Sorry, you don't have permission to access this page!
              </Typography>
            </motion.div>
            <Typography sx={{ color: 'text.secondary' }}>
              Sorry, you don’t have permission to access this page. Please contact to administrator for more information.
            </Typography>

            <motion.div variants={varBounceIn}>
              <Box
                component="img"
                src="/static/illustrations/403-removebg-preview.png"
                sx={{ height: 260, mx: 'auto', my: { xs: 5, sm: 10 } }}
              />
            </motion.div>

            <Button to="/" size="large" variant="contained" component={RouterLink}>
              Go to Home
            </Button>
            &nbsp;&nbsp;&nbsp;&nbsp;
            <Button size="large" variant="contained" onClick={async () => {
               const user = await userManager.getUser();
               localStorage.removeItem('user');
               userManager.signoutRedirect({ id_token_hint: user.id_token });
               userManager.removeUser(); // removes the user data from sessionStorage
            }}>
              Logout
            </Button>
          </Box>
        </MotionContainer>
      </Container>
    </RootStyle>
  );
}
