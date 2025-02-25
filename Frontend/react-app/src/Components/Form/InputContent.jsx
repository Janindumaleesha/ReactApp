import React from 'react'
import Box from '@mui/material/Box';
import TextField from '@mui/material/TextField';

function InputContent() {
  return (
    <Box
      component="form"
      sx={{ '& > :not(style)': { m: 1, width: '30ch' } }}
      noValidate
      autoComplete="off"
    >
      <TextField id="filled-basic" label="Filled" variant="filled" />
    </Box>
  )
}

export default InputContent