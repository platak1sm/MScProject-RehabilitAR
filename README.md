# RehabilitAR: Activity-Aware AR Instructions for Rehabilitation 

## Overview
**RehabilitAR** is an activity-aware augmented reality (AR) application designed for the **Meta Quest 3** to assist in shoulder rehabilitation. Developed in Unity 2022.3, the system provides a cost-effective, home-based solution for performing clinically relevant exercises without the need for external tracking hardware.

By leveraging passthrough AR and inverse kinematics (IK), RehabilitAR offers real-time guidance and feedback to improve exercise adherence and form.

---

## Key Features
* **Tracker-Free Guidance:** Utilizes the Quest's built-in hand and controller tracking rather than expensive external sensors.
* **Multi-Layered Visual Aids:** 
    * **Virtual Mirror:** An IK-mapped avatar reflects the user's movements in real-time for posture correction.
    * **3D Avatar Demonstration:** Provides looping animations showing the correct exercise form.
    * **Trainer Videos:** Embedded real-world video clips for technique reference.
* **Activity-Aware Feedback:** 
    * **Automatic Repetition Counting:** Validates arm positions against target angles to track progress.
    * **Real-time Overlays:** Displays green overlays for successful reps and red overlays if movements are too fast.
* **Passthrough AR:** Maintains environmental awareness and user comfort by blending virtual instructions with the real world.

---

## Exercise Selection
The application includes four specific exercises proven effective for deltoid strengthening and shoulder mobility:
1.  **Front Raises** 
2.  **Lateral Raises** 
3.  **Arm Circles**
4.  **Front-to-Side Raises** 

---

## Technical Specifications
* **Platform:** Meta Quest 3 
* **Engine:** Unity 2022.3 (Meta Quest 3 SDK & XR Interaction Toolkit) 
* **Tracking Logic:** Inverse Kinematics (IK) via the `UpperBodyIKDualConfig.cs` script 
* **Validation:** Movements are validated within an `angleTolerance` (typically 25-30 degrees)

---

## Controls
* **Y Button:** Toggle textual instruction UI.
* **A Button:** Skip current exercise.
* **B Button:** Exit application.

---

## Research & Results
A user study with 10 participants (ages 20-30) validated the application's effectiveness: 
* **System Usability Scale (SUS):** Achieved an average score of **81**, categorized as "Excellent".
* **Strengths:** High scores for exercise clarity (Q4), transition smoothness (Q12), and intuitive button controls (Q9).
* **Areas for Improvement:** Refinement of repetition tracking accuracy for diverse user body types and fitness levels.

---

## Demo
* [RehabilitAR](https://drive.google.com/file/d/1LNZCTaoDuLn6pMqCivZL33oBt07j-suo/view)

## Author
**Emmanouil Platakis** (202402638)
**Supervisor:** Stefanie Zollmann
**Institution:** Aarhus University, Department of Computer Science 
**Date:** May 2025 
