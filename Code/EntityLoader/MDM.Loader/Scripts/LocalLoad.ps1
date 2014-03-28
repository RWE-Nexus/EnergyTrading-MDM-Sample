$env:NEXUS_MDM_URI = "http://localhost:8090"
$env:NEXUS_DATASET_PATH = "..\..\..\..\..\Data\Candidate\DataSets"
$env:NEXUS_DATASET_NAMES_NO_UPDATE = "ProductsManual"
$env:NEXUS_DATASET_NAMES_WITH_UPDATE = "PDM Clean Party Data"

.\TeamCityLoad.ps1