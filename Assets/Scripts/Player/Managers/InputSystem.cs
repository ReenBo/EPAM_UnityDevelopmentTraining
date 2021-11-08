using ET.Interface.IComand;
using ET.Player.Skills;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ET.Player.InputSystem
{
    public class InputSystem : MonoBehaviour
    {
        private ICommand _recoveryHealthCommand;
        private ICommand _command;

        private Action _onNoneEvent = null;

        private PlayerSkillsController _skillsController;

        private Dictionary<KeyCode, ICommand> _commands;

        protected void Start()
        {
            _skillsController = GetComponent<PlayerSkillsController>();

            _recoveryHealthCommand = _skillsController;

            _commands = new Dictionary<KeyCode, ICommand>()
            {
                { KeyCode.Q, _recoveryHealthCommand }
            };
        }

        protected void Update()
        {
            ActivateCommand();
        }

        private void ActivateCommand()
        {
            foreach (var command in _commands)
            {
                if (Input.GetKeyDown(command.Key))
                {
                    SetOnLaunch(command.Value);
                }
            }
        }

        public void SetOnLaunch(ICommand command)
        {
            command.ExecuteCommand();
        }
    }
}