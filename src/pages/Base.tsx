import React, { Suspense, lazy } from 'react';
import { Switch, Route } from 'react-router-dom';

import Sidebar from '../components/common/Sidebar/Sidebar';
import Navbar from '../components/common/Navbar';
import PrivateRoute from '../components/common/PrivateRoute';

const Home = lazy(() => import('./Home/Home'));

const Base = () => {
    return (
        <PrivateRoute>
            <div className='page-wrapper'>
                <Sidebar />
                <div className='page-container'>
                    <Navbar />
                    <div className='main-content'>
                        <div className='section__content section__content--p30'>
                            <div className='container-fluid'>
                                <Suspense fallback={<div>Loading</div>}>
                                    <Switch>
                                        <Route exact path='/' component={Home} />
                                    </Switch>
                                </Suspense>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </PrivateRoute>
    );
}

export default Base;