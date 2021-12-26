import React, { Component, useState } from 'react';
import { Tree, TreeNode } from 'react-organizational-chart';
import { v4 as uuidv4 } from 'uuid';

import {
  Button, Modal, ModalFooter,
  ModalHeader, ModalBody
} from "reactstrap";

export function AddAdventurePath(props) {
    const [tree, setTree] = useState({});

    const [currentNode, setCurrentNode] = useState(null);

    const [currentQuestion, setCurrentQuestion] = useState({
      Question: '',
      CurrentAnswer: '',
      Answers : []
    });

    const [modal, setModal] = React.useState(false);
    const toggle = () => {
      setModal(!modal);
      setCurrentQuestion({
        Question: '',
        CurrentAnswer: '',
        Answers : []
      })
    };

    const AddPath = async (path) => {
        let url = '/api/adventure/addpath';

        await fetch(url, {
            method: "POST",
            body: JSON.stringify(path),
            headers: {
                "Content-type": "application/json; charset=UTF-8"
            }
        });
    }

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
    
    const AddNode = (node) => {  
      

      setCurrentNode(node);
     
      if(node.hasOwnProperty("isQuestion")) {
        if(!node.isQuestion && node.children.length === 1) {
          return;
        }
      }

      toggle();
      return;
    }

    const RenderTree = ({tree}) => {
      if(!tree)
        return '';

        return <TreeNode label={<div className={tree.isQuestion ? 'question' : ''}>{tree.label}</div>}>
        { tree.children && tree.children.map((element, index) => {
          return <RenderTree tree={element} />
        })}

        <TreeNode label={<div onClick={() => AddNode({...tree})}>Add</div>}>
        </TreeNode> 

      </TreeNode>
    }

    const setCurrentAnswer = (e) => {
      setCurrentQuestion({...currentQuestion, CurrentAnswer: e.target.value})
    }

    const setQuestion = (e) => {
      setCurrentQuestion({...currentQuestion, Question: e.target.value})
    }

    const AddAnswer = () => {
      let curr = {...currentQuestion};
      currentQuestion.CurrentAnswer !== '' && curr.Answers.push(currentQuestion.CurrentAnswer);
      curr.CurrentAnswer = '';
      setCurrentQuestion(curr);
    }

    const ConfirmQuestion = async () => {
      let newTree = { ...tree};
      let newPath = {};
      
      if(!newTree.id) {
        let parentId = uuidv4();
        newTree = {
          id : parentId,
          parentId: null,
          name: "1",
          isQuestion: true,
          level: 1,
          label: currentQuestion.Question,   
          children: []
        };
    
        newPath = {
          "id": parentId,
          "adventureID": props.match.params.id,
          "previousAnswer": null,
          "question":  currentQuestion.Question,
          "level": 1,   
          options: []
        };

        currentQuestion.Answers.forEach((element, index) => {
          let childID = uuidv4();
          newTree.children.push({
            id : childID,
            parentId: parentId,
            name: "1." + (index + 1),
            isQuestion: false,
            level: 2,
            label: element,   
            children: []
          });

          newPath.options.push({
            id: childID,
            pathID: parentId,
            label: element
          });
        });
        
        await AddPath(newPath);
        setTree(newTree);
        toggle();
        setModal(false);
        return;
      }

      let node = searchTree(newTree, currentNode.name);

      let newNodeName = node.name + '.' + (node.children.length + 1);

      if(!node.isQuestion && node.children.length === 1) {
        return;
      }
      
      let parentId = uuidv4();
      node.children.push({
        id : parentId,
        parentId: currentNode.id,
        name: newNodeName,
        isQuestion: !node.isQuestion,
        label: currentQuestion.Question,   
        level: node.level + 1,
        children:[]   
      });

      newPath = {
        "id": parentId,
        "adventureID": props.match.params.id,
        "previousAnswer": currentNode.id,
        "question":  currentQuestion.Question,
        "level":  node.level + 1,   
        options: []
      };

      currentQuestion.Answers.forEach((element, index) => {
        let childID = uuidv4();
        node.children[0].children.push({
          id : childID,
          parentId: parentId,
          name: newNodeName + "." + (index + 1),
          isQuestion: node.isQuestion,
          level: node.level + 2,
          label: element,   
          children: []
        });

        newPath.options.push({
          id: childID,
          pathID: parentId,
          label: element
        });
      });

      await AddPath(newPath);
      setTree(newTree);
      setModal(false)
      toggle();
    }

    return <Tree
      lineWidth={'2px'}
      lineColor={'green'}
      lineBorderRadius={'10px'}
      label={props.match.params.name}
    >
      <RenderTree tree={tree}/>

      <Modal isOpen={modal}
                toggle={toggle}
                modalTransition={{ timeout: 200 }}>
                <ModalBody>
                  <div>Question: <input onChange={setQuestion} value={currentQuestion.Question}/></div>
                  <div>Answer: <input onChange={setCurrentAnswer} value={currentQuestion.CurrentAnswer}/></div>
                  
                  <button onClick={AddAnswer}> Add Answer</button>
                  
                  <br/>
                  <br/>
                  
                  <div>
                    <b>Question -  {currentQuestion.Question}</b>

                  </div>
                  <div>
                    <b>Answers -</b>
                    <div>
                      {currentQuestion.Answers.map((element, index) => {
                        
                       return <div>{element}</div>
                      })}
                    </div>
                  </div>
                  
                  <div>
                    <button onClick={ConfirmQuestion} >Confirm</button>
                  </div>
                </ModalBody>
            </Modal>
    </Tree>
}