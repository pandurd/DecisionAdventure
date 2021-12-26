import React, { Component, useEffect, useState } from 'react';
import { Tree, TreeNode } from 'react-organizational-chart';


export function ShowUserAdventure(props) {
    const [tree, setTree] = useState({});

    const GetJourneyDetails = async () => {
      let url = `/api/userjourney/decisiontree?userJourneyID=${props.match.params.id}`;

      let advs = await fetch(url);
      let advsJson = await advs.json();
      setTree(advsJson);
    }

    useEffect(() => {
      GetJourneyDetails();
    }, []);

    const RenderTree = ({tree}) => {
      if(!tree)
        return '';

      let nodeClassName = tree.isSelected ? 'Selected' : '';

        if (tree.isQuestion)
            nodeClassName = nodeClassName + ' question';
        return <TreeNode label={<div className={nodeClassName}>{tree.label}</div>}>
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