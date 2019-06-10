# Small sample app for testing the Microsoft AutoML framework

The code is inspired by the following tutorial:  [Auto generate a binary classifier using the CLI](https://docs.microsoft.com/dotnet/machine-learning/tutorials/mlnet-cli) and Github repo [here](https://github.com/dotnet/machinelearning-samples)

The code is based on training data from 30k labelled comments on Wikipedia ('Wikipedia Detox'). The data consists of a tab-separated values file (.tsv) with each line containing a comment and a value of either 1 or 0 representing whether it is an offensive comment or not. 3 different sizes of training data (100, 1000 and 30000 samples) enable testing of various levels of data quality.

## Prerequisites

1) Install `.NET Core SDK 2.2` or later from [here](https://dotnet.microsoft.com/download/dotnet-core/2.2)

2) Install `ML.NET CLI` by entering the following in you terminal (on Windows you might have to log out/in after installing .NET Core SDK in order to have paths correctly setup to recognize the 'dotnet tool' command):

```console
> dotnet tool install -g mlnet
```

3) (Optional) Install VS Code from [here](https://code.visualstudio.com/Download)

## Running the sample

1) cd into the root directory of this repo where this README.md file is placed.

2) Train a simple model using the small dataset and using a short amount of time:

```console
> mlnet auto-train --task binary-classification --dataset wiki100Rows.tsv --label-column-name Label --max-exploration-time 30
```

This will attempt training a number of models within the given time and should output a generated model with accuracy around 83% placed in 'SampleBinaryClassification⁩/SampleBinaryClassification.Model⁩/MLModel.zip'

3) Run the custom console app 'CustomBinaryClassificationConsoleApp' in order to test the generated model:

```console
> cd CustomBinaryClassificationConsoleApp
> dotnet run
```

You should notice that some senetences are badly predicted such as "you are pretty" which is categorized as hostile.

4) Now try training a model with the large dataset using the following command (note the longer training time of 180 seconds due to the larger dataset):

```console
> cd ..
> mlnet auto-train --task binary-classification --dataset wiki30000Rows.tsv --label-column-name Label --max-exploration-time 180
```

This should result in a much better performing model with accuracy around 96% which you can test again by running the commands:

```console
> cd CustomBinaryClassificationConsoleApp
> dotnet run
```

And now you should hopefully experience a much better prediction regarding whether or not the sentences entered are hostile.