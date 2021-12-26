import React, { Component } from 'react';
import { AdventureList } from './AdventureList';
import { UserAdventureList } from './UserAdventureList';

export class Home extends Component {
  static displayName = Home.name;

  render () {
    return (
      <div>
        Available adventures

        <AdventureList /> 

        <UserAdventureList /> 
      </div>
    );
  }
}
