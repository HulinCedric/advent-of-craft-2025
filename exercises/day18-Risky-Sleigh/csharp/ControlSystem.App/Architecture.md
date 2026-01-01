## Architecture Overview
```mermaid
flowchart TB
    subgraph "Application Layer"
        APP["ControlSystem.App<br/>Program.cs"]
    end

    subgraph "Core Business Logic Layer"
        CS["ControlSystem<br/>Main Controller"]
        DASH["Dashboard<br/>Display Output"]
        RPU["ReindeerPowerUnit<br/>Power Management"]
        FACTORY["BestMagicalPerformance<br/>PowerUnitFactory"]
        AMP["MagicPowerAmplifier<br/>Power Boost"]

        subgraph "Enums & Models"
            STATUS["SleighEngineStatus"]
            ACTION["SleighAction"]
            AMPTYPE["AmplifierType"]
        end

        subgraph "Exceptions"
            EX1["SleighNotStartedException"]
            EX2["ReindeersNeedRestException"]
        end

        INT["IPowerUnitFactory<br/>Interface"]
    end

    subgraph "External Dependencies Layer"
        STABLE["MagicStable<br/>Reindeer Provider"]
        DEER["Reindeer<br/>External Entity"]
    end

    subgraph "Testing Layer"
        TEST["ControlSystem.Tests<br/>TestControlSystem"]
    end

    APP --> CS
    CS --> DASH
    CS --> RPU
    CS --> FACTORY
    CS --> STABLE
    CS --> STATUS
    CS --> ACTION
    CS --> EX1
    CS --> EX2

    FACTORY -.->|implements| INT
    FACTORY --> RPU
    FACTORY --> AMPTYPE
    FACTORY --> STABLE

    RPU --> AMP
    RPU --> DEER

    AMP --> AMPTYPE

    STABLE --> DEER

    TEST -.->|tests| CS

    style APP fill:#e1f5ff
    style CS fill:#ffe1e1
    style STABLE fill:#f0ffe1
    style TEST fill:#ffe1f5
```