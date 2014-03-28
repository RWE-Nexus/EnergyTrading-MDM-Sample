function Get-EntityLoadOrder() 
{
    return @(
		"Agreement.xml",
        "Calendar.xml",
        "Commodity.xml",
        "CommodityInstrumentType.xml",
        "Location.xml",
        "TenorType.xml",
        "Tenor.xml",
        "Market.xml",
        "Curve.xml",
        "Party.xml",
        "Person.xml",
        "Desk.xml",
        "Broker.xml",
        "Exchange.xml",
        "BusinessUnit.xml",
        "LegalEntity.xml",
        "LegalEntityPartyRole.xml",
        "PermanentEstablishmentPartyRole.xml",
        "PartyAccountabilityLEBU.xml",
        "Product.xml",
        "ProductTenorType.xml",
        "ProductCurve.xml",
        "Portfolio.xml",
		"Book.xml",
		"BookDefault.xml"
    )
}

function Get-EntityName([string] $entityFileName)
{
    switch ($entityFileName)
    {
        "Desk.xml" { return "PartyRole" }
        "LegalEntityPartyRole.xml" { return "PartyRole" }
        "PermanentEstablishmentPartyRole.xml" { return "PartyRole" }
        "PartyAccountabilityLEBU.xml" { return "PartyAccountability" }
        default
        {
            return $entityFileName.Replace(".xml", "")
        }
    }
}

function Determine-EntityOrder($knownMdmEntityOrder, [string] $dataSetPath) 
{
    $candidatesInOrder = @()
    $candidateEntityFiles = Get-ChildItem $dataSetPath\*.* -include *.xml -name
    
    $knownMdmEntityOrder | ForEach-Object {
        if ($candidateEntityFiles -contains $_) {
            $candidatesInOrder += $_
        }
    }
    return $candidatesInOrder
}

function Import-CandidateDataSet([string] $loaderExe, [string] $mdmUri, $fullEntityOrder, [string] $rootPath, [string] $dataSetName, [string] $logPath, [switch] $runAsCandidate) 
{
    Write-Host "Running MDM Loader to load data set: $dataSetName into MDM at: $mdmUri"

    $dataSetPath = Join-Path $rootPath $dataSetName

    if (-not (Test-Path $dataSetPath)) {
        throw "The data set path $dataSetPath does not exist"
    }

    $entitiesInOrder = Determine-EntityOrder $fullEntityOrder $dataSetPath

    $entitiesInOrder | ForEach-Object { 
        Load-CandidateEntity $loaderExe $mdmUri $dataSetPath $_ -runAsCandidate:$runAsCandidate
        Rename-LoaderLogFile $logPath $_
    }
}

function Load-CandidateEntity([string] $loaderExe, [string] $mdmUri, [string] $dataSetPath, [string] $candidateEntityFile, [switch] $runAsCandidate)
{
    $filePath = Join-Path $dataSetPath $candidateEntityFile

    $entityName = Get-EntityName $candidateEntityFile

    Write-Host "Loading candidate entity: $entityName in file: $filePath to MDM at: $mdmUri"
    
	if ($runAsCandidate)
	{
		Write-Host "With Candidate"
	    & $loaderExe -entity $entityName -file $filePath -mdm $mdmUri -y -cd
	}
	else
	{
		Write-Host "Without Candidate"
	    & $loaderExe -entity $entityName -file $filePath -mdm $mdmUri -y
	}

    if (-not $?) {
        throw "MDM load failed for: $filePath"
    }
}

function Remove-LoaderLogFiles([string] $logPath)
{
    if (Test-Path $logPath)
    {
        Get-ChildItem $logPath -include *.csv -recurse | Remove-Item
    }
}

function Rename-LoaderLogFile([string] $logPath, [string] $entityFileName) 
{
    $latestLogFile = Get-ChildItem $logPath\*.* -include MDMLoaderLog*.csv | Sort-Object LastWriteTime -descending | Select-Object -first 1
    $newLogFileName = $latestLogFile.Name.Replace("MDMLoaderLog", $entityFileName)
    Rename-Item $latestLogFile $newLogFileName
}

function Save-ErrorReport([string] $logPath, [string] $errorFileName)
{
    Get-ChildItem $logPath -Filter *.csv | 
    Select-String -Pattern "error" -AllMatches -SimpleMatch | 
    Foreach-Object {$_.Line} | 
    Out-File $errorFileName

    If ((Get-Content $errorFileName) -eq $Null) {
        Write-Host "No errors found"
        Remove-Item $errorFileName
    }
    Else {
        Write-Host "Load completed with errors - see $errorFileName"
    }
}

Export-ModuleMember Get-EntityLoadOrder, Import-CandidateDataSet, Remove-LoaderLogFiles, Save-ErrorReport