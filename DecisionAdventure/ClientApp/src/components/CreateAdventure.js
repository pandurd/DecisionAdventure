import React, { Component, useState } from 'react';
import { useHistory } from "react-router-dom";

export function CreateAdventure() {
    const history = useHistory();

    const [name, setName] = useState('');

    const CreateAdventure = async () => {
        var url = '/api/adventure/create';

        await fetch(url, {
            method: "POST",
            body: JSON.stringify({
                name: name
            }),
            headers: {
                "Content-type": "application/json; charset=UTF-8"
            }
        }).then(response => response.json())
        .then((res) => {
            history.push({
                pathname: `/add-adventure-path/${res.id}/${res.name}`
              })
        });
    }

    return <div>
        Name:
        <input name="adventureName" onChange={e => setName(e.target.value)} value={name} />

        <button onClick={CreateAdventure}>Create</button>
    </div>
}