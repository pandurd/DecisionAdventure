import React, { Component, useEffect, useState } from 'react';
import { Tree, TreeNode } from 'react-organizational-chart';
import { useHistory } from "react-router-dom";

export function StartAdventure(props) {
    const [tree, setTree] = useState({});
    const history = useHistory();

    const [selectedLevel, setSelectedLevel] = useState([]);

    const [journeyID, setJourneyID] = useState(null);

    const [currentNode, setCurrentNode] = useState(null);

    
    function searchTree(element, matchingTitle) {
      if(element.name == matchingTitle){
           return element;
      }else if (element.children != null){
           var i;
           var result = null;
           for(i=0; result == null && i < element.children.length; i++){
                result = searchTree(element.children[i], matchingTitle);
           }
           return result;
      }
      return null;
    }
    

    const start = async () => {
      let url = `/api/userjourney/start`;

        let res = await fetch(url, {
            method: "POST",
            body: JSON.stringify({
              id :props.match.params.id ,
              name: props.match.params.name
            }),
            headers: {
                "Content-type": "application/json; charset=UTF-8"
            }
        });

        let resJson = await res.json();
        
        setJourneyID(resJson.newJourneyID);

        let path =  {
          id : resJson.firstPath.id,
          parentId: resJson.firstPath.previousAnswer,
          name: "1",
          isQuestion: true,
          level: resJson.firstPath.level,
          label: resJson.firstPath.question, 
          children: []
        };

        resJson.firstPath.options.forEach((element, index) => {
          path.children.push({
            id : element.id,
            parentId: resJson.firstPath.id,
            name: "1." + (index + 1),
            isQuestion: false,
            level: 2,
            label: element.label,   
            children: []
          })
        });

        setTree(path);
    }

    useEffect(() => {
      start();
    }, [])

    const getNextStep = async (pathID, optionID, name) => {
      let url = `/api/userjourney/selectstep`;

        let res = await fetch(url, {
            method: "POST",
            body: JSON.stringify({
              userJourneyID : journeyID ,
              adventureID: props.match.params.id,
              currentPathID:  pathID,
              selectedOptionID: optionID
            }),
            headers: {
                "Content-type": "application/json; charset=UTF-8"
            }
        });

        let resJson = await res.json();
        
        let newTree = {...tree};
        let answerNode = searchTree(newTree, name);

        answerNode.children = [];

        answerNode.children[0] = {
          id : resJson.id,
          parentId: answerNode.id,
          name: answerNode.name + ".1",
          isQuestion: true,
          label: resJson.question,   
          level: resJson.level,
          children:[]   
        };

        resJson.options.map((element, index) => {
          answerNode.children[0].children.push({
            id : element.id,
            parentId: resJson.id,
            name: answerNode.name + ".1." + (index + 1),
            isQuestion: false,
            level: resJson.level + 1,
            label: element.label,   
            children: []
          });
        });

        setTree(newTree);

        if (resJson.options.length === 0) {
            history.push({
                pathname: `/showjourney/${journeyID}/${props.match.params.adventureName}`
            })
        }
        
    }

    const handleClick = async (node) => {
      if(node.isQuestion)
        return;

      if(selectedLevel.includes(node.level))
        return;

      setSelectedLevel([...selectedLevel, node.level]);
      await getNextStep(node.parentId, node.id, node.name);
    }

    const RenderTree = ({tree}) => {
      if(!tree)
        return '';

        let nodeClassName = '';

        if (tree.isQuestion)
            nodeClassName = nodeClassName + ' question';
        return <TreeNode label={<div className={nodeClassName} onClick={() => handleClick(tree)}>{tree.label}</div>}>
        { tree.children && tree.children.map((element, index) => {
          return <RenderTree tree={element} />
        })}
      </TreeNode>
    }

    return <Tree
      lineWidth={'2px'}
      lineColor={'green'}
      lineBorderRadius={'10px'}
      label={props.match.params.adventureName}
    >
      <RenderTree tree={tree}/>

    </Tree>
}