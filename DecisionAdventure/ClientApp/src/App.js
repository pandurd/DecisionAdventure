import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';

import { CreateAdventure } from './components/CreateAdventure';
import { AddAdventurePath } from './components/AddAdventurePath';
import { AdventureList } from './components/AdventureList';

import './custom.css'
import { StartAdventure } from './components/StartAdventure';
import { UserAdventureList } from './components/UserAdventureList';
import { ShowUserAdventure } from './components/ShowUserAdventure';

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
        <Route exact path='/' component={Home} />

        <Route path='/create-adventure' component={CreateAdventure} />
        <Route path='/add-adventure-path/:id/:name' component={AddAdventurePath} />

        <Route path='/available-adventure' component={AdventureList} />
        <Route path='/start-adventure/:id/:name/:adventureName' component={StartAdventure} />
      
        <Route path='/journeys' component={UserAdventureList} />
        <Route path='/showjourney/:id/:adventureName' component={ShowUserAdventure} />
      </Layout>
    );
  }
}
