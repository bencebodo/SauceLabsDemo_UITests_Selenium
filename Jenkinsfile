pipeline {
    agent {
        docker { 
            image 'mcr.microsoft.com/dotnet/sdk:10.0' 
            args '-u root' 
        }
    }

    stages {
        stage('Checkout') {
            steps {
                echo 'Pull repository...'
                checkout scm
            }
        }

        stage('Setup Environment') {
            steps {
                echo 'Installing Google Chrome for Selenium...'
                sh '''
                    apt-get update
                    apt-get install -y wget gnupg
                    wget -q -O - https://dl-ssl.google.com/linux/linux_signing_key.pub | apt-key add -
                    sh -c 'echo "deb [arch=amd64] http://dl.google.com/linux/chrome/deb/ stable main" >> /etc/apt/sources.list.d/google.list'
                    apt-get update
                    apt-get install -y google-chrome-stable
                '''
            }
        }

        stage('Restore & Build') {
            steps {
                echo 'Build solution...'
                sh 'dotnet restore'
                sh 'dotnet build --configuration Release'
            }
        }

        stage('UI tests') {
            steps {
                script{
                    updateGitHubStatus('ci/jenkins/ui-tests', 'UI tests started...', 'PENDING')
                }

                catchError(buildResult: 'FAILURE', stageResult: 'FAILURE') {
                    sh 'dotnet test SauceDemo_UI_Tests/SauceDemo_UI_Tests.csproj --configuration Release --logger "junit;LogFilePath=TEST-results.xml"'
                }
        
                script {
                    if (currentBuild.result == 'FAILURE') {
                        updateGitHubStatus('ci/jenkins/ui-tests', 'UI tests failed!', 'FAILURE')
                    } else {
                        updateGitHubStatus('ci/jenkins/ui-tests', 'UI tests passed!', 'SUCCESS')
                    }
                }
            }
        }
    }

    post {
        always {
            echo 'Pipeline finished. Publishing test results...'
            
            junit '**/TEST-results.xml'
        }
    }
}

def updateGitHubStatus(String contextName, String msg, String state) {
    step([
        $class: 'GitHubCommitStatusSetter',
        contextSource: [
            $class: 'ManuallyEnteredCommitContextSource',
            context: contextName
        ],
        statusResultSource: [
            $class: 'ConditionalStatusResultSource', 
            results: [
                [
                    $class: 'AnyBuildResult', 
                    message: msg, 
                    state: state
                ]
            ]
        ]
    ])
}