import React, { Component, useEffect, useState } from 'react';
import { useHistory } from "react-router-dom";

export function AdventureList() {
    const history = useHistory();

    const [adventures, setAdventures] = useState([]);
    const [user, setUser] = useState('testuser');

    const GetAdventures = async () => {
        let url = '/api/adventure/getall';

        let advs = await fetch(url);
        let advsJson = await advs.json();
        setAdventures(advsJson);
    }

    const StartAdventure = (id, adventureName) => {
        history.push({
            pathname: `/start-adventure/${id}/${user}/${adventureName}`
          })
    }

    useEffect(() => {
        GetAdventures();
    }, [])

    return <div>
        <b>Username </b> 
        <input onChange={e => setUser(e.target.value)} value={user} />
        <br />
        <br />
     
        <div><b>List of available Adventures</b></div>
        <br />
       
        {adventures && adventures.length > 0 && adventures.map((element, index) => {
            return <div> {element.name} <button onClick={() => StartAdventure(element.id, element.name)}>Take</button></div>
        })}
    </div>
}