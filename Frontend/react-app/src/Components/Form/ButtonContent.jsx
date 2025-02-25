import React from 'react'
import Box from '@mui/material/Box';
import Button from '@mui/material/Button';

function ButtonContent() {
  return (
    <Box
      component="form"
      sx={{ '& > :not(style)': { m: 1, width: '30ch' } }}
      noValidate
      autoComplete="off"
    >
        <Button variant="contained">Contained</Button>
    </Box>
  )
}

export default ButtonContent