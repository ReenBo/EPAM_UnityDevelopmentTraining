using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem
{
    private int _levelUp = 1;
    private int _currentLevel = 1;

    private float _amountExperienceRaiseLevel = 100;
    private float _levelConversionModifier = 0.2f;

    private float _currentExperience = 0;
    private int _minExperience = 0;
    private int _maxExperience = 0;


    public void CalculateExperiencePlayer(float experience)
    {
        _currentExperience += experience;

        _maxExperience = (int)_amountExperienceRaiseLevel; //

        if (_currentExperience >= _maxExperience)
        {
            _currentExperience = _minExperience;

            _amountExperienceRaiseLevel += _amountExperienceRaiseLevel * _levelConversionModifier;

            _currentLevel += _levelUp;
        }

        //_playerViem.SetExpView(experience, _currentExperience, _maxExperience, _currentLevel);
    }
}
