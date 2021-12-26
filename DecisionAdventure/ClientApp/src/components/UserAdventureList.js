import React, { Component, useEffect, useState } from 'react';
import { useHistory } from "react-router-dom";

export function UserAdventureList() {
    const history = useHistory();

    const [journeys, setJourneys] = useState([]);
    const [user, setUser] = useState('testuser');


    const Getjourneys = async () => {
        let url = '/api/userjourney/journeys';

        let advs = await fetch(url);
        let advsJson = await advs.json();
        setJourneys(advsJson);
    }

    const Show = (id, adventureName) => {
        history.push({
            pathname: `/showjourney/${id}/${adventureName}`
          })
    }

    useEffect(() => {
        Getjourneys();
    }, [])

    return <div>
        <br />
        <br />
   
        <b>Journeys Taken</b>
        
        { journeys && journeys.length > 0  && journeys.map((element, index) => {
            return <div> {element.id} - {element.userName}  - {element.adventureName} <button onClick={() => Show(element.id, element.adventureName)}>Show Decision Tree</button></div>
        })}
    </div>
}