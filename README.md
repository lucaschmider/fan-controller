# FanController

This repository aims to create an external fan controller board with accompanying software on the 
embedded and pc side. It consists of multiple projects with different purposes:

* `Firmware` contains the source code of an embedded controller that monitors and controls the fan 
  speed according to external control signals
* `TestData` contains notebooks that aim to answer upcoming questions with empirical data
* `Controller` contains the source code for various software services that provide the necessary 
  control signals for the `Firmware` project