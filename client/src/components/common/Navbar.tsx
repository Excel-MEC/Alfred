import React from 'react';

const Navbar = () => {
    return (
        <header className='header-desktop'>
            <div className='section__content section__content--p30'>
                <div className='container-fluid'>
                    <div className='header-wrap'>
                        <form className='form-header' action='' method='POST'>
                            <input className='au-input au-input--xl' type='text' name='search'
                                placeholder='Search for datas &amp; reports...' />
                            <button className='au-btn--submit' type='submit'>
                                <i className='zmdi zmdi-search'></i>
                            </button>
                        </form>
                        <div className='header-button'>
                            <div className='noti-wrap'>
                                <div className='noti__item js-item-menu'>
                                    <div className='noti__item js-item-menu'>
                                    </div>
                                </div>
                            </div>
                            <div className='account-wrap'>
                                <div className='account-item clearfix js-item-menu'>
                                    <div className='image'>
                                        <img src={require('../../assets/img/avatar-01.jpg')} alt='John Doe' />
                                    </div>
                                    <div className='content'>
                                        <a id='GFG' className='js-acc-btn' href='#'>john doe</a>
                                    </div>
                                    <div className='account-dropdown js-dropdown'>
                                        <div className='info clearfix'>
                                            <div className='image'>
                                                <a href='#'>
                                                    <img src={require('../../assets/img/avatar-01.jpg')} alt='John Doe' />
                                                </a>
                                            </div>
                                            <div className='content'>
                                                <h5 className='name'>
                                                    <a id='GFG' href='#'>john doe</a>
                                                </h5>
                                                <span className='email'>johndoe@example.com</span>
                                            </div>
                                        </div>
                                        <div className='account-dropdown__body'>
                                            <div className='account-dropdown__item'>
                                                <a id='GFG' href='#'>
                                                    <i className='zmdi zmdi-account'></i>Account
                                                </a>
                                            </div>
                                            <div className='account-dropdown__item'>
                                                <a id='GFG' href='#'>
                                                    <i className='zmdi zmdi-settings'></i>Setting
                                                </a>
                                            </div>

                                        </div>
                                        <div className='account-dropdown__footer'>
                                            <a id='GFG' href='#'>
                                                <i className='zmdi zmdi-power'></i>Logout
                                            </a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </header>
    );
}

export default Navbar;