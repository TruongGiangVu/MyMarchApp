import { Button, CircularProgress, ButtonProps } from "@mui/material";

interface IProps extends ButtonProps {
  loading?: boolean;
}

export default function LoadingButton({ loading, children, ...props }: IProps) {
  return (
    <Button {...props} disabled={loading || props.disabled}>
      {loading ? <CircularProgress size="1.5em" color="inherit" /> : children}
    </Button>
  );
}