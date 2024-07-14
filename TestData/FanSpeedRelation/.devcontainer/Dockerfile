FROM quay.io/jupyter/datascience-notebook:latest

USER root
# Generally, Dev Container Features assume that the non-root user (in this case jovyan)
# is in a group with the same name (in this case jovyan). So we must first make that so.
RUN groupadd jovyan \
    && usermod -g jovyan -a -G users jovyan

USER jovyan

# Set an environment variable for the get_data.py script to detect whether it is running in docker.
ENV IS_DOCKER=true