import React, { Fragment } from 'react';
import { NavLink } from 'react-router-dom';
import { routes } from './routes';

const SidebarRoutes = () => (
    <Fragment>
        {
            routes.map((route) => {
                if (!route.hasOwnProperty('subList')) {
                    return (
                        <li>
                            <NavLink id='GFG' to={route.path}>
                                <i className={route.icon}></i>{route.name}
                            </NavLink>
                        </li>
                    )
                } else {
                    return (
                        <li className='has-sub'>
                            <NavLink id='GFG' className='js-arrow' to={route.path}>
                                <i className='fas fa-copy'></i>Pages
                                                </NavLink>
                            <ul className='list-unstyled navbar__sub-list js-sub-list'>
                                {
                                    route.subList?.map((subRoute) => (
                                        <li>
                                            <NavLink id='GFG' to={subRoute.path}>{subRoute.name}</NavLink>
                                        </li>
                                    ))
                                }
                            </ul>
                        </li>
                    )
                }
            })
        }
    </Fragment>
)

const Sidebar = () => {
    return (
        <Fragment>
            <header className='header-mobile d-block d-lg-none'>
                <div className='header-mobile__bar'>
                    <div className='container-fluid'>
                        <div className='header-mobile-inner'>
                            <NavLink className='logo' to='/'>
                                <img src={require('../../../assets/img/logo.png')} alt='CoolAdmin' />
                            </NavLink>
                            <button className='hamburger hamburger--slider' type='button'>
                                <span className='hamburger-box'>
                                    <span className='hamburger-inner'></span>
                                </span>
                            </button>
                        </div>
                    </div>
                </div>
                <nav className='navbar-mobile'>
                    <div className='container-fluid'>
                        <ul className='navbar-mobile__list list-unstyled'>
                            <SidebarRoutes />
                        </ul>
                    </div>
                </nav>
            </header>
            <aside className='menu-sidebar d-none d-lg-block'>
                <div className='logo'>
                    <a href='#'>
                        <img src={require('../../../assets/img/logo.png')} alt='Cool Admin' />
                    </a>
                </div>
                <div className='menu-sidebar__content js-scrollbar1'>
                    <nav className='navbar-sidebar'>
                        <ul className='list-unstyled navbar__list'>
                            <SidebarRoutes />
                        </ul>
                    </nav>
                </div>
            </aside>
        </Fragment>
    );
}

export default Sidebar;