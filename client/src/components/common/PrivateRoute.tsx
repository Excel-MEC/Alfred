const isLoggedIn = () => true;

interface Props {
    children: any;
}

const PrivateRoute = ({ children }: Props) => {
    if (isLoggedIn()) {
        return children;
    }
};

export default PrivateRoute;