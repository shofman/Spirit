using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

/**
 * Class that handles the state that a mouse can be in
 */
public class RandomGenerator {
    /**
     * Singleton pattern - we want only one instance of a mouse state per game
     */
    private static RandomGenerator _instance;

    // Random number generation (DO NOT INSTANTIATE MULTIPLE TIMES FOR BETTER RANDOMNESS)
    private System.Random random;

    /**
     * Constructor - set to private to prevent accidental use
     */
    private RandomGenerator() {
        random = new System.Random();
    }

    /**
     * Statically access the current mouse state
     * @return Mouse - the current object (if initially null, we create a new Mouse)
     */
    public static RandomGenerator instance() {
        if (_instance == null) {
            _instance = new RandomGenerator();
        }
        return _instance;
    }

    /**
     * Gets a number random number to use
     * @param {[type]} int - The highest maximum number that we want to generate
     * @return {[type]} int - a new random number
     */
    public int getRandomNumber(int numberOfItems) {
        return random.Next(numberOfItems);
    }
}