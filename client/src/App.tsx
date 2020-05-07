import React from 'react';
import { BrowserRouter as Router, Switch, Route } from 'react-router-dom';

import Base from './pages/Base';

const App = () => {
    return (
        <Router>
            <Switch>
                <Route path='/' component={Base} />
            </Switch>
        </Router>
    );
};

export default App;
